#include "PointManager.h"

void PointManager::AddPoint(int x, int y) {
    points.push_back({ x, y });
}

void PointManager::RemovePoint(int index) {
    if (index >= 0 && index < (int)points.size())
        points.erase(points.begin() + index);
}

bool PointManager::GetPoint(int index, int* x, int* y) {
    if (index >= 0 && index < (int)points.size()) {
        *x = points[index].x;
        *y = points[index].y;
        return true;
    }
    return false;
}

int PointManager::Count() {
    return (int)points.size();
}