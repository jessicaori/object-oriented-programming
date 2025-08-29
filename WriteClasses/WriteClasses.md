# Write classes

## Definition

- A class is a blueprint that defines the structure and behavior that objects created from it will have.

## Real-world Example

Imagine we want to represent a Test Case in an automation framework:

- A test case has attributes like Id, Name, Steps.
- It also has behavior liek `Execute()` that actually runs the test.
- The id once the test is created shouldn't be modified and also it is required.
- The Name can be modified after the test has been initialized
- The Steps can be modified after the test has been initialized 

## When creating a class...

...it is important to remember that we need to defined depending on the context.
...since the context is important, most of the time diagrams can save us time when looking at a class.
