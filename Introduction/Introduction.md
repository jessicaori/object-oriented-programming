# 1. An Introduction to Object-Oriented

## 1.1. Class basics and benefits

### What is a class?

A class is like blueprint or a template that defines how objects should look and behave that are from the real-world. 

> They are known as entities

A class defines:

- Attributes (fields/properties): data it holds.
- Methods (functions): actions it can perform.

### What is an object ?

An object is an instance of a class - created based on the bluprint or template.

> Instance: TODO: Define what is an instance in your own words.

### Note

At the end of day a class is like a test case template, and objects as the executed test cases with actual data.

### Benefits of classes

- Reusability: Once defined, you can reuse the class in different parts of your code.
- Abstraction: Hide unnecessary details, show only what matters.
- Maintainability: Code is easier to manage and update.
- Scalability: Easy to extend and adapt to future needs.
- Encapsulation: Bundle data & behavior together according to a context.

## 1.2. Creating objects using the constructor

### What is a constructor? (Definition)

A constructor is a special method that is automatically called when an object of a class is created.

- It *usually* initializes fields or properties of the class
- Constructors do not have a return type (not even `void`)
- If you don't explicitly define one, C# provides a default constructor.
