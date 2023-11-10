using System;

public class Calculator<T>
{
    public delegate T BinaryOperation(T a, T b);

    public BinaryOperation Addition { get; } = (a, b) => (dynamic)a + b;
    public BinaryOperation Subtraction { get; } = (a, b) => (dynamic)a - b;
    public BinaryOperation Multiplication { get; } = (a, b) => (dynamic)a * b;
    public BinaryOperation Division { get; } = (a, b) => (dynamic)a / b;

    public T PerformOperation(BinaryOperation operation, T a, T b)
    {
        return operation(a, b);
    }
}

