using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TaskCli.Models;
using CliTaskStatus = TaskCli.Enums.TaskStatus;


Console.WriteLine("Welcome to the Task Manager that will manage your life");

var filePath = Path.Combine(
    Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.FullName,
    "Task-Storge.json"
);

var options = new JsonSerializerOptions { WriteIndented = true };


List<TaskItem> tasks = new List<TaskItem>();


if (File.Exists(filePath))
{
    var content = File.ReadAllText(filePath);
    if (!string.IsNullOrWhiteSpace(content))
        tasks = JsonSerializer.Deserialize<List<TaskItem>>(content) ?? new List<TaskItem>();
}



while (true)
{
    Console.WriteLine("Enter a CLI command:");
    var input = Console.ReadLine();

    

    var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
    var cmd = parts[0].ToLower();




    if (cmd == "exit")
    {
        string json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText(filePath, json);

        Console.WriteLine($"Saved {tasks.Count} task(s) to: {filePath}");
        break;
    }




    else if (cmd == "add")
    {
        tasks.Add(new TaskItem(Guid.NewGuid(), parts[1]));
     Console.WriteLine(tasks[0].ToString());
    }
    
    else if (cmd == "list")
    {
        foreach (var task in tasks)
        {
            Console.WriteLine(task.ToString());
        }
    }

    else if (cmd == "update")
    {
        foreach (var task in tasks)
        {
            if (task.Id.ToString() == parts[1].Split(' ', 2)[0])
            {
                task.UpdateTask(parts[1].Split(' ', 2)[1]);
                Console.WriteLine("Task updated successfully.");
            }
        }
    }

    else if (cmd == "mark")
    {   
        Console.WriteLine("done , inprogress");
        var input2 = Console.ReadLine();
        foreach (var task in tasks)
        {
            if (task.Id.ToString() == parts[1].Split(' ', 2)[0])
            {
                if (input2 == "done")
                {
                    task.UpdateStatus(CliTaskStatus.Done);
                }
                else if (input2 == "inprogress")
                {
                    task.UpdateStatus(CliTaskStatus.InProgress);
                }
                Console.WriteLine("Task updated successfully.");
            }
        }
    }

    

    


    else
    {
        Console.WriteLine("Unknown command.");
    }
}
