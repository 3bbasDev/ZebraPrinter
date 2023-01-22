using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;

namespace ZebraPrinterSDK.Repositories;
public interface IPrinterRepo
{
    Task Print(string text);
}
internal sealed class PrinterRepo : IPrinterRepo
{
    private readonly IConfiguration configuration;

    public PrinterRepo(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task Print(string text)
    {
        // Instantiate connection for ZPL TCP port at given address
        Zebra.Sdk.Comm.Connection thePrinterConn = new TcpConnection(configuration["PrinterIP"], TcpConnection.DEFAULT_ZPL_TCP_PORT);

        try
        {
            // Open the connection - physical connection is established here.
            thePrinterConn.Open();

            // This example prints "This is a ZPL test." near the top of the label.
            string zplData = "^XA^FO20,20^A0N,25,25^FDThis is a ZPL test.^FS^XZ";

            // Send the data to printer as a byte array.
            await Task.Run(() => thePrinterConn.Write(Encoding.UTF8.GetBytes(zplData)));
        }
        catch (ConnectionException e)
        {
            Console.WriteLine(e.ToString());
        }
        catch (ZebraPrinterLanguageUnknownException e)
        {
            Console.WriteLine(e.ToString());
        }
        catch (IOException e)
        {
            Console.WriteLine(e.ToString());
        }
        finally
        {
            thePrinterConn.Close();
        }
    }
}
