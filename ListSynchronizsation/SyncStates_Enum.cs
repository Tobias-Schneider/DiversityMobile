namespace UBT.AI4.Bio.DivMobi.ListSynchronization
{
    public enum SyncStates_Enum
    {
        PrematureState = 0,
        IgnoreState = 1,
        InsertState = 2,
        UpdateState = 3, 
        DeletedState=4, 
        ConflictState=5, 
        ConflictResolvedState=6, 
        SynchronizedState=100
    }
}