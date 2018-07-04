using System;
using System.Collections.Generic;
using System.Linq;

delegate string Call();

class Menu {
  List<Element> Elements = new List<Element>();
  public string ExitCommand = "exit";
  public Call Callback;

  public Menu(Call cb) {
    Callback = cb;
  }
  public void Start() {
    Console.WriteLine("---------------------------");
    Console.WriteLine("List of available commands:\r\n");
    if (Callback != null) {
      Callback();
    }
    foreach (Element e in Elements) {
      Console.WriteLine(e);
    }
    Console.WriteLine("\r\nWrite '" + ExitCommand + "' to close the program.\r\n");
    string cmd = Console.ReadLine();
    if (cmd == ExitCommand) {
      Environment.Exit(0);
    }
    Console.WriteLine(Exec(cmd));
    Start();
  }
  public void Add(string name, string desc, Call call) {
    Elements.Add(new Element(name, desc, call));
  }

  public string Exec(string param) {
    foreach (Element el in Elements) {
      if (el.Name == param) {
        return el.Callback();
      }
    }
    return "";
  }
}

// Menu element
class Element {
  public string Description;
  public string Name;
  public Call Callback;
  public Element(string name, string desc, Call call) {
    Description = desc;
    Name = name;
    Callback = call;
  }
  public override string ToString() {
    return Name + " — " + Description;
  }
}