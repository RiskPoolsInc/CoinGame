namespace App.Data.Criterias.Core.Helpers; 

public static class StateArrayExclude {
    public static StateArrayExcludeEnum GetStateArrayExclude(this Array array, bool exclude) {
        return exclude
            ? array.Length == 1
                ? StateArrayExcludeEnum.NotArrayAndExclude
                : StateArrayExcludeEnum.ArrayAndExclude
            : array.Length == 1
                ? StateArrayExcludeEnum.NotArrayAndNotExclude
                : StateArrayExcludeEnum.ArrayAndNotExclude;
    }
}