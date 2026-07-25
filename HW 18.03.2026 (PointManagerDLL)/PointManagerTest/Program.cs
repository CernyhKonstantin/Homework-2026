using System;
using System.Runtime.InteropServices;

class Program
{
    const string DllName = "PointManagerDLL.dll";

    [DllImport(DllName)]
    public static extern IntPtr CreatePointManager();

    [DllImport(DllName)]
    public static extern void DestroyPointManager(IntPtr pm);

    [DllImport(DllName)]
    public static extern void PointManager_AddPoint(IntPtr pm, int x, int y);

    [DllImport(DllName)]
    public static extern void PointManager_RemovePoint(IntPtr pm, int index);

    [DllImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool PointManager_GetPoint(IntPtr pm, int index, out int x, out int y);

    [DllImport(DllName)]
    public static extern int PointManager_Count(IntPtr pm);

    static void Main()
    {
        IntPtr pm = CreatePointManager();

        PointManager_AddPoint(pm, 10, 20);
        PointManager_AddPoint(pm, 30, 40);
        PointManager_AddPoint(pm, 50, 60);

        Console.WriteLine($"Count: {PointManager_Count(pm)}");

        for (int i = 0; i < PointManager_Count(pm); i++)
        {
            PointManager_GetPoint(pm, i, out int x, out int y);
            Console.WriteLine($"Point {i}: ({x},{y})");
        }

        PointManager_RemovePoint(pm, 1);
        Console.WriteLine("After removal:");

        for (int i = 0; i < PointManager_Count(pm); i++)
        {
            PointManager_GetPoint(pm, i, out int x, out int y);
            Console.WriteLine($"Point {i}: ({x},{y})");
        }

        DestroyPointManager(pm);
    }
}