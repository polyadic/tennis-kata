# Tennis Kata

This solution is based on Mark Seemans solution to the Tennis Kata

https://blog.ploeh.dk/2016/02/10/types-properties-software/

It has been chosen as an example on how to work with Funcky.DiscriminatedUnion.

It is a direct translation of the F# solution to C# but not with the Isomorphic [Visitor-Pattern](https://blog.ploeh.dk/2018/06/25/visitor-as-a-sum-type/).
instead we use Funcky.DiscriminatedUnion to create automatically exhaustive Match (and Switch) functions via the C# generator.

This reduces the boiler-plate code to an absolut minimum, and it is viable solution to using DiscriminatedUnions more often in C#.
