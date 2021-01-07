import java.lang.reflect.Array;

public class Main {

    public static void main(String[] args) {
        int arr[] = (int[]) Array.newInstance(int.class, 5); // a new instance is created using the Array.newInstance() method.
        Array.set(arr, 0, 2); // We set the values of the array with the Array.set() method.
        Array.set(arr, 2, 2);
        Array.set(arr, 1, 9);
        Array.set(arr, 2, 3);
        Array.set(arr, 4, 7);
        System.out.print("The array elements are: ");
        for (int i : arr) {
            System.out.print(i + " ");

        }
    }
}
