namespace App.Common.Helpers;

public static class HashHelper {
    public static int GetHashCode<T1, T2>(T1 arg1, T2 arg2) {
        unchecked {
            return 31 * arg1.GetHashCode() + arg2.GetHashCode();
        }
    }

    public static int GetHashCode<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {
        unchecked {
            var hash = arg1.GetHashCode();
            hash = 31 * hash + arg2.GetHashCode();
            return 31 * hash + arg3.GetHashCode();
        }
    }

    public static int GetHashCode<T1, T2, T3, T4>(T1 arg1,
                                                  T2 arg2,
                                                  T3 arg3,
                                                  T4 arg4) {
        unchecked {
            var hash = arg1.GetHashCode();
            hash = 31 * hash + arg2.GetHashCode();
            hash = 31 * hash + arg3.GetHashCode();
            return 31 * hash + arg4.GetHashCode();
        }
    }

    public static int GetHashCode<T>(T[] list) {
        unchecked {
            var hash = 0;

            foreach (var item in list)
                hash = 31 * hash + item.GetHashCode();
            return hash;
        }
    }

    public static int GetHashCode<T>(IEnumerable<T> list) {
        unchecked {
            var hash = 0;

            foreach (var item in list)
                hash = 31 * hash + item.GetHashCode();
            return hash;
        }
    }

    /// <summary>
    ///     Gets a hashcode for a collection for that the order of items
    ///     does not matter.
    ///     So {1, 2, 3} and {3, 2, 1} will get same hash code.
    /// </summary>
    public static int GetHashCodeForOrderNoMatterCollection<T>(IEnumerable<T> list) {
        unchecked {
            var hash = 0;
            var count = 0;

            foreach (var item in list) {
                hash += item.GetHashCode();
                count++;
            }

            return 31 * hash + count.GetHashCode();
        }
    }
}