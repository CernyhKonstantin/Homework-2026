#include "PointManager.h"

extern "C" {

    PointManager* CreatePointManager() {
        return new PointManager();
    }

    void DestroyPointManager(PointManager* pm) {
        delete pm;
    }

    void PointManager_AddPoint(PointManager* pm, int x, int y) {
        pm->AddPoint(x, y);
    }

    void PointManager_RemovePoint(PointManager* pm, int index) {
        pm->RemovePoint(index);
    }

    bool PointManager_GetPoint(PointManager* pm, int index, int* x, int* y) {
        return pm->GetPoint(index, x, y);
    }

    int PointManager_Count(PointManager* pm) {
        return pm->Count();
    }

}