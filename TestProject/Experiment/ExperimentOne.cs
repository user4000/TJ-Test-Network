using System;
using System.Collections.Generic;
using TJSettings;

namespace TestProject
{
  public class ExperimentOne
  {
    public List<Folder> ListFolders = new List<Folder>();

    public void AddRandomFolder()
    {
      ListFolders.Add
        (new Folder
          (
          Faker.RandomNumber.Next(1, 1000),
          Faker.Company.Name() + " " + Faker.Address.StreetName() + " " + Faker.Internet.UserName(),
          Faker.Phone.Number() + Faker.Name.FullName() + " " + Faker.Company.Name(),
          Faker.RandomNumber.Next(1, 1000)
          )
        );
    }

    public void AddRandomFolder(int count)
    {
      for (int i = 1; i <= count; i++) AddRandomFolder();
    }
  }
}
