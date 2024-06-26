namespace App.Data.Criterias.Core.Helpers; 

public abstract class AFilterArrayCriteria<T, S> : ACriteria<S> where S : class {
    protected readonly bool Exclude;
    protected readonly T[] FilterItems;

    protected AFilterArrayCriteria(T[] filterItems, bool exclude = false) {
        FilterItems = filterItems;
        Exclude = exclude;
    }
}