using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ConsoleApp1
{
  class Program
  {
    static TODOList list = null;
    static void Main(string[] args) {
      Menu menu = new Menu(() => {
        if (list != null) {
          Console.WriteLine("=> Working with file: " + list.Path + "\r\n");
        }
        return "\r\n";
      });
      menu.Add("create", "Create new todo file", () => {
        Console.WriteLine("Name:");
        list = TODOList.Create(Console.ReadLine());
        return "\r\nTODO File created\r\n";
      });
      menu.Add("add", "Create new todo in list", () => {
        OpenList().Add(NewEntityDialog());
        OpenList().Save();
        return "\r\nTask successfully saved\r\n";
      });
      menu.Add("show", "Print todo with id", () => {
        try {
          Console.WriteLine(EntityById(OpenList()).ToString());
          return "\r\nTask successfully printed\r\n";
        }
        catch (Exception e) {
          Console.WriteLine(e);
          return "\r\nFail";
        }
      });
      menu.Add("edit", "Edit todo with id", () => {
        try {
          TODOList l = OpenList();
          TODOEntity e = EntityById(l);
          Console.WriteLine(e.ToString());
          TODOEntity newEntity = NewEntityDialog();
          newEntity.ID = e.ID;

          l.Update(newEntity);
          l.Save();
          return "\r\nTask successfully updated\r\n";
        }
        catch (Exception e) {
          Console.WriteLine(e);
          return "Fail";
        }
      });
      menu.Add("remove", "Remove todo with id from list", () => {
        try {
          TODOList l = OpenList();
          TODOEntity e = EntityById(l);
          Console.WriteLine(e.ToString());
          l.Remove(e);
          // l.Save();
          return "\r\nSuccessfully removed";
        }
        catch (Exception e) {
          Console.WriteLine(e);
          return "\r\nFail";
        }
      });
      menu.Add("showall", "Show all todos", () => {
        try {
          OpenList().Print();
          Console.WriteLine("\r\nPress enter to return to the menu.");
          Console.Read();
          return "\r\nSuccessfully printed all todos\r\n";
        }
        catch (Exception e) {
          Console.WriteLine(e);
          return "\r\nFail";
        }
      });
      menu.Start();
    }

    static public TODOList OpenList() {
      if (list == null) {
        Console.Write("\r\nFile name: ");
        list = TODOList.Open(Console.ReadLine());
        return list;
      }
      return list;
    }

    static public TODOEntity EntityById(TODOList list) {
      Console.Write("ID: ");
      int.TryParse(Console.ReadLine(), out int id);
      if (id == 0) {
        throw new Exception("ID must a number");
      }
      Console.WriteLine();
      return list.ReadById(id);
    }

    static public TODOEntity NewEntityDialog() {
      Console.WriteLine("Task name:");
      string name = Console.ReadLine();
      Console.WriteLine("Task status:");
      string status = Console.ReadLine();
      Console.WriteLine("Task description:");
      string desc = Console.ReadLine();
      return new TODOEntity(0, name, status, desc);
    }
  }
}
