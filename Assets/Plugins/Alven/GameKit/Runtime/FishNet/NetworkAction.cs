#if FISHNET
namespace Alven.GameKit.FishNet
{
    public delegate void NetworkAction(bool asServer);
    public delegate void NetworkAction<in T>(T value, bool asServer);
    public delegate void NetworkAction<in T1, in T2>(T1 value1, T2 value2, bool asServer);
    public delegate void NetworkChangedAction<in T>(T prev, T next, bool asServer);
}
#endif