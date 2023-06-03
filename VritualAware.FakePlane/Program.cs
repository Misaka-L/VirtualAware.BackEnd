const string BaseUrl = "http://localhost:5035";

var httpClient = new HttpClient() {
    BaseAddress = new Uri(BaseUrl)
};

Console.Write("World: ");
var world = Console.ReadLine();
Console.Write("Instance: ");
var instace = Console.ReadLine();
Console.Write("Callsign: ");
var callsign = Console.ReadLine();
Console.Write("Reg: ");
var registration = Console.ReadLine();
Console.Write("Type: ");
var typeCode = Console.ReadLine();
Console.Write("Alt: ");
var altitude = int.Parse(Console.ReadLine());
Console.Write("Range: ");
var range = int.Parse(Console.ReadLine());

var heading = 0;

var latitude = 0;
var longitude = 0;

var rev = false;

while (true) {
    if (rev) {
        if (latitude >= 0) {
            heading = 180;
            latitude = latitude - 128;
        } else if (longitude >= 0) {
            heading = 270;
            longitude = longitude - 128;
        } else {
            rev = false;
        }
    } else {
        if (latitude <= range) {
            heading = 0;
            latitude = latitude + 128;
        } else if (longitude <= range) {
            heading = 90;
            longitude = longitude + 128;
        } else {
            rev = true;
        }
    }

    try {
        await httpClient.GetAsync($"/flightradar/track?" +
            $"Callsign={callsign}&" +
            $"TypeCode={typeCode}&" +
            $"Registration={registration}&" +
            $"Altitude={altitude}&" +
            $"InstanceId={instace}&" +
            $"WorldId={world}&" +
            $"Latitude={latitude}&" +
            $"Longitude={longitude}&" +
            $"Heading={heading}&" +
            $"Groundspeed=250");
        Console.WriteLine($"Update: {callsign} - {typeCode} / {registration} | long: {longitude}, lat: {latitude}, hdg: {heading}");
    } catch (Exception ex) {
        Console.WriteLine(ex);
        Console.ReadKey();
    }

    await Task.Delay(TimeSpan.FromSeconds(1));
}