#region struct
//public struct student
//{
//    public int _id;
//    public string _name;

//    public string Name
//    {
//        get { return _name; }
//        set { _name = value; }
//    }
//    public int Id
//    {
//        get { return _id;}
//        set { _id = value;}
//    }
//    public student(int  Id, string Name)
//    {
//        this._id = Id;
//        this._name = Name;
//    }
//    public void PrintDetails()
//    {
//        Console.WriteLine("id - {0} && Name - {1}", this._id, this._name);
//    }
//    //~student()
//    //{
//    //    //Struct cant have destructor
//    //}
//}
//class Program
//{
//     static void Main()
//    {
//        //student s1 = new student(101, "Vguru");//here we initailize in parameter
//        //s1.PrintDetails();

//        //student s2 = new student();//here we didnot initailize the we get 0 and blank null value
//        //s2.Id = 20;/// here we initailize in properties
//        //s2.Name = "Raksha";
//        //s2.PrintDetails();

//        //student s3 = new student
//        //{
//        //    Id = 33,
//        //    Name = "Sushmitha"
//        //};
//        //s3.PrintDetails();
//    }
//}
#endregion

#region Interface
//interface Student
//{

//    void print()
//    {

//    }
//}
//class Program : Student
//{
//    public void print()
//    {
//        Console.WriteLine("hello");
//    }
//    static void Main()
//    {
//        Student s = new Program();
//        s.print();


//    }
//}
#endregion

#region Exception
//class Program
//{
//    public static void print()
//    {
//        Console.WriteLine("hello");
//    }
//    static void Main()
//    {
//        try
//        {
//            StreamReader sr = new StreamReader(@"C:\Users\Hp\source\repos\introductionClass\Text.txt");
//            Console.WriteLine(sr.ReadToEnd());
//            sr.Close();
//        }
//        catch(FileNotFoundException ex)
//        {
//            Console.WriteLine(ex.Message);
//            Console.WriteLine(ex.StackTrace);
//        }
//        catch(Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//            Console.WriteLine(ex.StackTrace);
//        }
//    }
//}
#endregion

#region  Test1
//class Program
//{
//    public static void print()
//    {
//        Console.WriteLine("hello");
//        Console.WriteLine("Hey");
//    }
//    static void Main()
//    {
//        try
//        {
//            StreamReader sr = new StreamReader(@"C:\Users\Hp\source\repos\introductionClass\Text.txt");
//            Console.WriteLine(sr.ReadToEnd());
//            sr.Close();
//        }
//        catch (FileNotFoundException ex)
//        {
//            Console.WriteLine(ex.Message);
//            Console.WriteLine(ex.StackTrace);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//            Console.WriteLine(ex.StackTrace);
//        }
//    }
//}
#endregion

#region Option_Parameter

//class Program
//{
//    public static void Main()
//    {
//        Add(1, 2);
//        Add(1, 2, 3, 4);
//        Add(1, 2, new object[] { 3, 4 });
//    }
//    public static void Add(int f, int s, params object[] res)
//    {
//        int result = f + s;
//        if(res != null)
//        {
//            foreach(int i in res)
//            {
//                result += i;
//            }
//        }
//        Console.WriteLine(result);
//    } 
//}
#endregion

#region dictionary
//class Program
//{
//    public static void Main()
//    {
//        print();
//    }
//    public static void print()
//    {
//        Dictionary<int, string> dict = new Dictionary<int, string>();
//        dict.Add(1, "b");
//        dict.Add(2, "d");
//        dict.Add(3, "e");
//        dict.Add(4, "f");
//        dict.Add(5, "g");
//        dict.Add(6, "h");
//        Console.WriteLine(dict.TryGetValue(1, out string str));
//        Console.WriteLine("Info " + dict.ToString());
//        Console.WriteLine("total count: " + dict.Count());
//        Console.WriteLine(dict.Remove(5));
//        Console.WriteLine("total count: " + dict.Count());
//        foreach (int key in dict.Keys)
//        {
//            Console.WriteLine(key);
//            Console.WriteLine(dict[key]);
//        }
//        foreach(string key in dict.Values)
//        {
//            Console.WriteLine(key);            
//        } 

//    }
//}
#endregion

#region List

//class Program
//{
//    public static void Main()
//    {
//        Customer c1 = new Customer
//        {
//            id = 1,
//            name = "Raksha",
//            salary = 100000
//        };
//        Customer c2 = new Customer
//        {
//            id = 2,
//            name = "vguru",
//            salary = 120000
//        };
//        Customer c3 = new Customer
//        {
//            id = 3,
//            name = "Divya",
//            salary = 50000
//        };

//        List<Customer> ListCust = new List<Customer>();
//        ListCust.Add(c1);
//        ListCust.Add(c2);
//        ListCust.Add(c3);
//        Console.WriteLine(ListCust.Contains(c3));
//        Console.WriteLine(ListCust.Exists(cust => cust.name.StartsWith("D")));
//        Customer customer = ListCust.Find(cust => cust.salary > 60000);
//        Console.WriteLine("ID - {0}, Name - {1}, Salary - {2}", customer.id, customer.name, customer.salary);
//        Customer c = ListCust.FindLast(cust => cust.salary > 60000);
//        Console.WriteLine("ID - {0}, Name - {1}, Salary - {2}", c.id, c.name, c.salary);
//        List<Customer> cu = ListCust.FindAll(cust => cust.salary > 60000);
//        foreach(var cl in cu)
//            Console.WriteLine("ID - {0}, Name - {1}, Salary - {2}", cl.id, cl.name, cl.salary);
//    }
    
//}

//public class Customer
//{
//    public int id { get; set; }
//    public string name { get; set; }

//    public int salary { get; set; }
//}
#endregion