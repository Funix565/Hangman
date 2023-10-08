# File I/O cheat sheet

The main namespace is `System.IO`.

| Class | Meaning |
|-------|---------|
|[`BinaryReader`](https://learn.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-7.0) <br />[`BinaryWriter`](https://learn.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-7.0)| Work with data types in binary files. `ReadXXXX()`, highly overloaded `Write()`s etc.|
|[`Directory`](https://learn.microsoft.com/en-us/dotnet/api/system.io.directory?view=net-7.0) (*static*) <br />[`DirectoryInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.io.directoryinfo?view=net-7.0) (*object*)| Work with directories. Create, move etc.|
|[`DriveInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.io.driveinfo?view=net-7.0)| Get info about system drives.|
|[`File`](https://learn.microsoft.com/en-us/dotnet/api/system.io.file?view=net-7.0) (*static*) <br />[`FileInfo`](https://learn.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=net-7.0) (*object*)| Work with files. Create, copy, delete, move, open etc.|
|[`FileStream`](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-7.0)| Primitive class to work with file content, read, write, seek. Data in raw bytes.|
|[`FileSystemWatcher`](https://learn.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-7.0)| Monitor changes in files and directories.|
|[`Path`](https://learn.microsoft.com/en-us/dotnet/api/system.io.path?view=net-7.0)| Cross-platform operations and information on string path information.|
|[`StreamWriter`](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=net-7.0) <br />[`StreamReader`](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-7.0)| Work with text data in files.|

## Cross-platform path syntax
`Path.VolumeSeparatorChar`, `Path.DirectorySeparatorChar`.
For example:

```csharp
DirectoryInfo dir3 = new DirectoryInfo($@"C{Path.VolumeSeparatorChar}{Path.DirectorySeparatorChar}MyCode{Path.DirectorySeparatorChar}Testing");
```

## Additional File-centric Members

| Method | Meaning |
|--------|---------|
|`ReadAllBytes()`| Opens the file, reads all data as an array of bytes, closes the file.|
|`ReadAllLines()`| Opens the file, reads all data as an array of strings, closes the file.|
|`ReadAllText()`| Opens the file, reads all data as one `String`, closes the file.|
|`ReadLines()`| **A better approach because returns `IEnumerable` collection.**|
|`WriteAllBytes()`| Opens the file, writes the byte array, closes the file.|
|`WriteAllLines()`| Opens the file, writes the string array, closes the file.|
|`WriteAllText()`| Opens the file, writes one string, closes the file.|

### Write and read with minimal fuss:

```csharp
Console.WriteLine("** Simple I/O with File");
string[] myTasks = {"Fix", "Call", "Play"};

// Write out all data to file on C drive.
File.WriteAllLines(@"tasks.txt", myTasks);

// Read it all back and print out.
foreach (string tasks in File.ReadAllLines(@"tasks.txt"))
{
    Console.WriteLine("TODO: {0}", task);
}
Console.ReadLine();
File.Delete("tasks.txt");
```

## Working with FileStreams

It is an implementation for the abstract `Stream` class. It can read or write only a single byte or an array of bytes.
`FileStream` can operate only on raw bytes.

```csharp
using System.Text;

Console.WriteLine("***** Fun with FileStreams *****\n");

using (FileStream fStream = File.Open("myMessage.dat", FileMode.Create))
{
  // Encode a string as an array of bytes.
  string msg = "Hello!";
  byte[] msgAsByteArray = Encoding.Default.GetBytes(msg);

  // Write byte[] to file.
  fStream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

  // Reset internal position of stream.
  fStream.Position = 0;

  // Read the types from file and display to console.
  Console.Write("Your message as an array of bytes: ");
  byte[] bytesFromFile = new byte[msgAsByteArray.Length];
  for (int i = 0; i < msgAsByteArray.Length; i++)
  {
    bytesFromFile[i] = (byte)fStream.ReadByte();
    Console.Write(bytesFromFile[i]);
  }

  // Display decoded messages.
  Console.Write("\nDecoded Message: ");
  Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
  Console.ReadLine();
}

File.Delete("myMessage.dat");
```

The main difficulty: we have to work with raw bytes.

## Working with StreamWriters and StreamReaders

We use these classes when we need to read or write strings. Default encoding is Unicode but we can change this via `System.Text.Encoding`.

Classes derive from an abstract type `TextWriter` and an abstract type `TextReader` accordingly.

### Writing to a Text File

The following code creates a new file. `File.CreateText()` method returns `StreamWriter` to write characters to the new file.

```csharp
Console.WriteLine("***** Fun with StreamWriter / StreamReader *****\n");

// Get a StreamWriter and write string data.
using (StreamWriter writer = File.CreateText("reminders.txt"))
{
    writer.WriteLine("Don't forget Mother's Day this year...");
    writer.WriteLine("Don't forget Father's Day this year...");
    writer.WriteLine("Don't forget these numbers:");
    for (int i = 0; i < 10; i++)
    {
      writer.Write(i + " ");
    }

    // Insert a new line.
    writer.Write(writer.NewLine);
}

Console.WriteLine("Created file and wrote some thoughts...");
Console.WriteLine();
```

Create file is located in `bin\Debug\net6.0` folder.

> Working with streams is like working with `System.Console`. However, we write textual data to a file, not to the standard output device.

### Reading from a Text File

Core Members
| Member | Meaning |
|--------|---------|
|`Read()`| Read one character.|
|`ReadLine()`| Read a line.|
|`ReadToEnd()`| Read all characters and return single string.|

```csharp
...
// Now read data from file.
Console.WriteLine("Here are your thoughts:\n");
using(StreamReader sr = File.OpenText("reminders.txt))
{
  string input = null;
  while ((input = sr.ReadLine()) != null)
  {
    Console.WriteLine(input);
  }
}
Console.ReadLine();
```

We can often achieve an identical result using different approaches.

We can create `StreamWriter` and `StreamReader` directly. For example:

```csharp
// Get a StreamWriter and write string data.
using(StreamWriter writer = new StreamWriter("reminders.txt"))
{
...........
}

// Now read data from file.
using(StreamReader sr = new StreamReader("reminders.txt"))
{
...........
}
```

## Summary

We can use static members from `File` class to easily read or write content.
We can use `StreamWriter` class to write textual data, we can write single character or lines.
We can use `StreamReader` class to read textual data, we can read single character, blocks, lines or all characters.
