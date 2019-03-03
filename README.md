# Kata01: Supermarket Pricing
This kata arose from some discussions here: http://codekata.com/kata/kata01-supermarket-pricing/

The problem domain is something seemingly simple: pricing goods at supermarkets.

Some things in supermarkets have simple prices: this can of beans costs $0.65. Other things have more complex prices. For example:

three for a dollar (so what’s the price if I buy 4, or 5?)
$1.99/pound (so what does 4 ounces cost?)
buy two, get one free (so does the third item have a price?)
This kata involves no coding. The exercise is to experiment with various models for representing money and prices that are flexible enough to deal with these (and other) pricing schemes, and at the same time are generally usable (at the checkout, for stock management, order entry, and so on).

# Goal
The goal of this kata is to practice a looser style of experimental modelling. Look for as many different ways of handling the issues as possible. Consider the various tradeoffs of each. What techniques are best for exploring these models? For recording them? How can you validate a model is reasonable?

### Design overview 
Let's start with a simple design without any complex pricing logic:
![domain](https://user-images.githubusercontent.com/9795243/53691777-a4461f80-3d39-11e9-90b3-01d78f1c21b6.png)

The next step is to design a more complex logic that enables us selecting an algorithm or strategy to calculate total price at runtime: <a href='https://en.wikipedia.org/wiki/Strategy_pattern'>Strategy pattern</a>. Instead of implementing a single strategy directly, like what we did in the previous approach, code receives runtime instructions as to which in a family of strategies to use.

Consdier a simple strategy that handles <a href='https://en.wikipedia.org/wiki/Bulk_purchasing'>volume pricing</a>: a pricing strategy that allows discount for bulk purchases. For example, “apple cost 50 cents, but if a customer wants three apples, the total cost is $1.30”.

As the behavior of pricing varies based on different strategies, we create two PricingStrategy classes to calculate the total price based on two strategies: regular and volume.

![strategy](https://user-images.githubusercontent.com/9795243/53692109-0bb29e00-3d3f-11e9-864f-249443371866.png)

Observe that PricingStrategy classes implement <i>IPricingStrategy</i> interface with a polymorphic GetTotal method. In this method, we pass OrderItem object as a parameter, so that strategy object can calculate regular price (pre-discount) and volume discount based on the number of items.

Let's have a look at sequence diagram:

![collaboration](https://user-images.githubusercontent.com/9795243/53692193-3a7d4400-3d40-11e9-896c-207e23be48cf.png)

The UML sequence diagram shows the runtime interaction: The context is OrderItem that delegates price calculation to a strategy object.

Now take a look at the context object, OrderItem, and its associated interface <i>IPricingStrategy</i> (not a concrete class):

![orderitem](https://user-images.githubusercontent.com/9795243/53692284-c2178280-3d41-11e9-9dc3-370f262fcce1.png)

There are different pricing algorithms or strategies, and they change over time. Who should create the strategy? A straightforward approach is to apply <a href='https://en.wikipedia.org/wiki/Factory_method_pattern'>Factory pattern</a>: a PricingStrategyFactory can be responsible for creating all strategies needed by the application. PricingStrategyFactory is also responsible for reading pricing rules from data store, since it is creating the pricing strategy.

To raise yet another interesting requirements and design problem: How do we handle the case of multiple, conflicting pricing policies? Is there a way to change the design so that the OrderItem object does not know if it is dealing with one or many pricing strategies, and also offer a design for the conflict resolution? Yes, with the <a href='https://en.wikipedia.org/wiki/Composite_pattern'>Composite pattern</a>.

For example, a new class called CompositeLowestPricingStrategy can implement the <i>IPricingStrategy</i> and itself contain other PricingStrategy objects. This is a signature feature of a composite object: The outer composite object contains a list of inner objects, and both the outer and inner objects implement the same interface. That is, the composite class itself implements the <i>IPricingStrategy</i> interface.

![composite](https://user-images.githubusercontent.com/9795243/53698455-ccfa0380-3d91-11e9-9e49-29a43fc9630e.png)


