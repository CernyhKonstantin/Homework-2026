#pragma once
#include <vector>

struct Point {
    int x;
    int y;
};

class PointManager {
private:
    std::vector<Point> points;
public:
    void AddPoint(int x, int y);
    void RemovePoint(int index);
    bool GetPoint(int index, int* x, int* y);
    int Count();
};

#ifdef POINTMANAGERDLL_EXPORTS
#define PM_API __declspec(dllexport)
#else
#define PM_API __declspec(dllimport)
#endif

extern "C" {
    PM_API PointManager* CreatePointManager();
    PM_API void DestroyPointManager(PointManager* pm);
    PM_API void PointManager_AddPoint(PointManager* pm, int x, int y);
    PM_API void PointManager_RemovePoint(PointManager* pm, int index);
    PM_API bool PointManager_GetPoint(PointManager* pm, int index, int* x, int* y);
    PM_API int PointManager_Count(PointManager* pm);
}