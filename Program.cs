/*
 Desfăşurare examen:
Citiți tot! (şi dacă nu vă propuneți să rezolvați tot) 
Probleme:
1. fişierul "data01.in" conține următoarele date:
5
1 4 7 9 10
8
3 3 5 7 8 19 20 22
Adică (2 vectori dați prin numărul de elemente (maxim 20000) şi apoi un şir cu elementele, numere întregi de maxim 4 cifre, ordonate crescător).
Proiectați un algoritm (eficient din punct de vedere al timpului de execuţie şi a memoriei utilizate) care construieşte şirul cu valorile pare din cele două şiruri în ordine crescătoare
Pt. exemplu ar fi:
4 8 10 20 22
Salvați şirul în fişierul "data1.out"
Explicați în fișierul "Help1.txt" în câteva cuvinte algoritmul folosit şi eficiența acestuia


2. Definim puterea unui vector ca fiind numărul maxim de valori identice urmate de următorul maxim de valori identice ş.a.m.d.
Ex1: 2,3,3,4,4,4,3 va avea puterea (3,3,1) deoarece există 3 de 4, 3 de 3 şi un 2.
Ex2: 1,1,1,1,1,1,1 va avea puterea (7) deoarece există 7 de 1
Ex3: 1,2,3,4,5,6,6 va avea puterea (2,1,1,1,1,1)
Ex4: 1,2,3,1,2,3,1,2,3 va avea puterea (3,3,3)
ştiind că valorile vectorului sunt numere întregi între -100 şi 100 iar dimensiunea vectorilor nu depăşeşte 1000000 de valori construiți o funcție care primeşte argument un vector şi returnează vectorul reprezentând puterea acestuia.
Construiți o funcție care primeşte argument 2 vectori (v şi u) şi returnează -1 dacă puterea lui u este mai mică decât v, 
1 în caz contrar şi 0 dacă cei doi vectori au puteri egale. 
Un vector are putere mai mare față de alt vector dacă valoarea cea mai semnificativă este mai mare decât valoarea corespunzătoare din vectorul al doi-lea.
în exemplele anterioare vectorul de la Ex2 are puterea cea mai mare (7), după care Ex4 (3,3,3), Ex1 (3,3,1) şi ultimul Ex3 (2,1,...).

3. în fişierul "data2.in" există o matrice n x m formată doar din valorile 0,1,2 şi 3. 
Construiți un mecanism care să identifice numărul de aglomerări (elemente adiacente, vecine de aceeaşi valoare) din fiecare valoare non 0 și să scrie aceste valori în fişierul "data2.out".
0001313111220
0001331102222
0301000222200
0333000020000
Răspuns:
3 1 3
Explicație, dacă urmăriți în matrice există 3 platouri de 1, un platou de valori 2 și 3 platouri de valoarea 3
*/

using System.Linq;
using System.Text.Json.Serialization.Metadata;

namespace AF_exam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //aici se schimba exercitiul executat
            Ex1();
            Ex2();
            Ex3();
        }

        static void Ex1()//varianta eficienta, complexitate O(n)
        {
            Console.WriteLine("Ex1: ");

            StreamReader sr = new StreamReader("data01.in");
            StreamWriter sw = new StreamWriter("data1.out");

            //citim datele conform enuntului
            int n = int.Parse(sr.ReadLine());
            int[] v1 = new int[n];
            string[] line1 = sr.ReadLine().Split(' ');
            for (int i = 0; i < line1.Length; i++)
                v1[i] = int.Parse(line1[i]);

            int m = int.Parse(sr.ReadLine());
            int[] v2 = new int[m];
            string[] line2 = sr.ReadLine().Split(' ');
            for (int i = 0; i < line2.Length; i++)
                v2[i] = int.Parse(line2[i]);

            // n - numarul de elemente din sirul v1 (vectorul 1)
            // m - numarul de elemente din sirul v2 (vectorul 2)

            //rezolvare
            int index1 = 0; //indexul cu care parcurgem primul sir
            int index2 = 0; //indexul cu care parcurgem cel de al doilea sir
            int index = 0; //indexul sirului interclasat

            int[] interclasat = new int[n+m];//sirul interclasat va contine toate elementele din cele doua siruri, impreunate

            while (index1 < n && index2 < m) // cat timp nu ajungem la finalul unuia dintre cele 2 siruri
            {
                if (v1[index1] < v2[index2]) // daca elementul curent din primul sir e mai mic ca cel din al doilea
                {
                    interclasat[index] = v1[index1]; //il adaugam pe v1[index1] in sirul interclasat
                    index++;
                    index1++;
                }
                else
                {
                    interclasat[index] = v2[index2];//il adaugam pe v2[index2] in sirul interclasat
                    index++;
                    index2++;
                }
            }

            //daca am ajuns aici e garantat ca cel putin unul dintre cele doua siruri a ajuns la final si trebuiesc copiate valorile ramase din celalalt in sirul interclasat
            //cum nu stim exact care sir s-a terminat, incercam sa copiem potentialele valori ramase in sirul interclasat, din fiecare sir

            while (index1 < n)
            {
                interclasat[index] = v1[index1]; //il adaugam pe v1[index1] in sirul interclasat
                index++;
                index1++;
            }

            while (index2 < m)
            {
                interclasat[index] = v2[index2];//il adaugam pe v2[index2] in sirul interclasat
                index++;
                index2++;
            }

            //dupa ce am obtinut sirul interclasat, in ordine crescatoare, afisam elementele pare din acesta, conform cerintei
            for(int i=0;i<interclasat.Length;i++) //parcurgem sirul interclasat
                if (interclasat[i] % 2 == 0) //daca elementul curent este par
                    Console.Write($"{interclasat[i]} "); // in mod normal scriem, sw.Write($"{interclasat[i]} "); (il scriem in fisierul de iesire)
        }

        static void Ex2()
        {
            Console.WriteLine();
            Console.WriteLine("Ex2: ");

            //prima parte
            int[] arr = { 2, 3, 3, 4, 4, 4, 3 };
            List<int> putereVector = GetPutereVector(arr);

            Console.Write("Cer1: ");
            foreach (int el in putereVector)
                Console.Write($"{el} ");
            Console.WriteLine();

            //a doua parte
            int[] arr1 = { 2, 3, 3, 4, 4, 4, 3 };
            int[] arr2 = { 1, 1, 1, 1, 1, 1, 1 };

            int vec1MaiMareDecatVec2 = CompararePutere(GetPutereVector(arr1), GetPutereVector(arr2));//rezultatul returnat trebuie sa fie -1, deoarece Putere(arr2) > Putere(arr1)
            Console.WriteLine($"Cer2: {vec1MaiMareDecatVec2}");
        }

        static List<int> GetPutereVector(int[] v)
        {
            List<int> toReturn = new List<int>(); //lista de putere a vectorului

            //valorile elementelor pot fi de la -100 la 100, cum nu putem avea vector de frecventa pt valori negative (ex v[-58]) o sa transpunem valorile la dreapta cu 100
            // -> v[-100] -> v[-100 + 100] = v[0], v[100] -> v[100 + 100] = v[200]
            int[] frecv = new int[200];

            for (int i = 0; i < v.Length; i++)
                frecv[v[i] + 100]++;

            BubbleSort(frecv); //sortam vectorul de frecventa

            for(int i = frecv.Length - 1; i>=0; i--) //alegem frecventele cele mai mari, sortate, diferite de 0, ex: 3 3 1
                if (frecv[i] > 0)
                    toReturn.Add(frecv[i]);

            return toReturn;
        }

        private static void BubbleSort(int[] v) //sorteaza vectorul crescator in O(n^2)
        {
            bool ok;
            do {
                ok = true;
                for(int i=0;i<v.Length-1;i++)
                {
                    if (v[i] > v[i+1])
                    {
                        (v[i], v[i + 1]) = (v[i+1], v[i]);
                        ok = false;
                    }
                }
            } while (!ok);
        }

        // -1 daca arr1 este mai slab decat arr2
        // 0 daca arr1 este egal cu arr2
        // 1 daca arr1 este mai tare decat arr2
        static int CompararePutere(List<int> arr1, List<int> arr2)
        {
            for(int i=0;i < Math.Min(arr1.Count, arr2.Count); i++)
            {
                if (arr1[i] > arr2[i])
                    return 1;
                else
                    if(arr2[i] > arr1[i])
                        return -1;
            }

            //daca sunt egale pana intr-un punct, dar listele au lungimi diferite, lista mai lunga are puterea mai mare, ex: 3 3 1 < 3 3 1 1 1 1
            if(arr1.Count > arr2.Count)
                return 1;
            else
                if (arr2.Count > arr1.Count)
                    return -1;

            return 0;
        }

        static void Ex3()
        {
            Console.WriteLine("Ex3: ");
            StreamReader sr = new StreamReader("data2.in");

            //citire
            List<string> buffers = new List<string>();
            string buffer;
            while ((buffer = sr.ReadLine()) != null)
                buffers.Add(buffer);

            int n = buffers.Count;
            int m = buffers[0].Length;

            int[,] mat = new int[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    mat[i, j] = buffers[i][j] - '0';

            //rezolvare
            int[] result = new int[4];

            for(int i=0; i<n; i++)
                for(int j=0;j<m;j++)
                    if (mat[i,j]!=0)//daca gasim o valoare diferita de 0 in matrice
                    {
                        result[mat[i, j]]++; //am gasit un nou platou, deci crestem frecventa acestui tip de platou
                        recFunc(i, j, mat[i, j], mat); //inlocuim toate valorile din platoul respectiv cu 0, prin intermediul acestei fct recursive
                    }

            for (int i = 1; i < 4; i++)
                Console.Write($"{result[i]} ");
        }

        static void recFunc(int i, int j, int value, int[,] mat)
        {
            if(i>=0 && i<mat.GetLength(0) && j>=0 && j<mat.GetLength(1)) //daca elementul curent se afla in interiorul matricei
            {
                if (mat[i,j]==value) //daca elementul curent face parte din platoul gasit
                {
                    mat[i, j] = 0; //il inlocuim cu 0

                    //cautam daca vecinii lui (N, E, S, V) fac parte din acelasi platou, caz in care continuam parcurgerea in directiile respective
                    recFunc(i-1, j, value, mat);
                    recFunc(i, j+1, value, mat);
                    recFunc(i+1, j, value, mat);
                    recFunc(i, j-1, value, mat);
                }
            }
        }
    }
}