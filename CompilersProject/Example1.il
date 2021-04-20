.class public auto ansi beforefieldinit Program extends [mscorlib]System.Object
{
  .method public hidebysig static void foo() cil managed
  {
    .maxstack 2
    .locals init (
    [0] int32 x,
    [1] int32 y,
    [2] bool V_1
    )
    nop
    nop
    ldc.i4.s 5
    stloc.s x
    ldc.i4.s 7
    stloc.s y
    br.s    IL_2

    IL_1: nop
    ldloc.s y
    ldloc.s x
    add
    stloc.s y


    IL_2:     ldloc.s x
    ldc.i4.s 0
    cgt
    stloc.s V_1

    ldloc.s    V_1
    brfalse.s    IL_1

    ret
  }

  .method public hidebysig specialname rtspecialname instance void
    .ctor() cil managed
  {
    .maxstack 8
    ldarg.0
    call         instance void [mscorlib]System.Object::.ctor()
    nop
    ret
  }
}