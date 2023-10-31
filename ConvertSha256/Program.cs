using ConvertSha256;

string output = string.Empty;

ConvertS256 c = new ConvertS256(ref output, DateTime.Now.ToString());

Console.WriteLine(output);