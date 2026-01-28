using System;
using UnityEngine;
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public double Multiplication(int a, int b)
    {
        return a * b;
    }

    public double Division(int dividend, int divisor)
    {
        double result = 0.0;
        try
        {
            result = dividend / divisor;
        }
        catch (DivideByZeroException e)
        {

        }
        return result;
    }
}
