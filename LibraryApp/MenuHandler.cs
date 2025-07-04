﻿using System.Dynamic;

class MenuHandler
{
    // const, private, protected, public

    #region printMenu
    private const string MENU_MAIN = @"
Silahkan Pilih Menu:
1. Buat Message
2. Lihat Message
3. Exit
    ";
    
    private const string MENU_MESSAGE = @"
Menu Message:
1. Message Only
2. Approval with Message
3. Approval Message with Files
4. Back to Main Menu
5. Exit
    ";
    #endregion

    #region olahInput
    
    private List<Message> messages = new List<Message>();

    private string? getInput(string message)
    {
        Console.Write(message);
        string? input = string.Empty;
        input = Console.ReadLine();
        return input;
    }

    private int parseInput(string? input)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        else
        {
            Console.WriteLine("Input tidak valid, silakan masukkan angka.");
            return -1;
        }
    }

    private string storeInput(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            Console.WriteLine("Input tidak boleh kosong");
            return getInput("Silakan masukkan input kembali: ");
        }
        else
        {
            return input;
        }
    }

    public abstract class Message
    {
        public string Pengirim { get; set; }
        public string Penerima { get; set; }
        public string Subject { get; set; }
        public string Isi { get; set; }

        public abstract void DisplayMessage();
    }

    public abstract class MessageOnly : Message
    {
        public override void DisplayMessage()
        {
            Console.WriteLine($"Pengirim: {Pengirim}");
            Console.WriteLine($"Penerima: {Penerima}");
            Console.WriteLine($"Subject: {Subject}");
            Console.WriteLine($"Isi: {Isi}");
        }
    }
    public abstract class ApprovalMessage : Message
    {
        public bool StatusApproval { get; set; }

                public override void DisplayMessage()
        {
            Console.WriteLine($"Pengirim: {Pengirim}");
            Console.WriteLine($"Penerima: {Penerima}");
            Console.WriteLine($"Subject: {Subject}");
            Console.WriteLine($"Isi: {Isi}");
            Console.WriteLine($"Status Approval: {(StatusApproval ? "Approved" : "Not Approved")}");
        }
    }
    #endregion

    private void showMainMenu()
    {
        while (true)
        {
            Console.WriteLine(MENU_MAIN);
            int pilihan = parseInput(getInput("Masukan Pilihan: "));

            try
            {
                switch (pilihan)
                {
                    case 1:
                        showMessageMenu();
                        break;
                    case 2:
                        showMessageList();
                        break;
                    case 3:
                        Console.WriteLine("Bye :D");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak valid, silakan coba lagi.\n");
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input tidak valid, masukan input kembali");
            }
        }
    }

    #region MenuBuatMessage
    
    private void showMessageMenu()
    {
        Console.WriteLine(MENU_MESSAGE);
        bool run = true;
        int pilihan = parseInput(getInput("Masukkan pilihan: "));

        try
        {
            while (run)
        {
            switch (pilihan)
            {
                case 1:
                    MenuMessageOnly();
                    run = false;
                    break;
                case 2:
                    MenuApprovalMessage();
                    run = false;
                    break;
                case 3:
                    MenuApprovalFiles();
                    run = false;
                    break;
                case 4:
                    run = false;
                    break;
                case 5:
                    Console.WriteLine("Bye :D");
                    Environment.Exit(0);
                    break;
                default:
                 Console.WriteLine("Pilihan tidak valid, silakan coba lagi.\n");
                 run = false;
                    break;
            }
        }
        }
        catch (FormatException e)
        {
            Console.WriteLine("Input tidak valid, masukan input kembali");
        }
    }

    private void MenuMessageOnly()
    {
        Console.WriteLine("Menu Message Only - Silahkan Isi Data Berikut:");
        string pengirim = storeInput(getInput("Masukan Nama Pengirim: "));
        string penerima = storeInput(getInput("Masukan Nama Penerima: "));
        string subject = storeInput(getInput("Masukan Nama Subject: "));
        string message = storeInput(getInput("Masukan Nama Message: "));

        messages.Add(new MessageOnly
        {
            Pengirim = pengirim,
            Penerima = penerima,
            Subject = subject,
            Isi = message,
        });

        Console.WriteLine("Input berhasil disimpan.");
    }

    private void MenuApprovalMessage()
    {
        Console.WriteLine("Menu Message Only - Silahkan Isi Data Berikut:");
        string pengirim = storeInput(getInput("Masukan Nama Pengirim: "));
        string penerima = storeInput(getInput("Masukan Nama Penerima: "));
        string subject = storeInput(getInput("Masukan Nama Subject: "));
        string message = storeInput(getInput("Masukan Nama Message: "));

        messages.Add(new ApprovalMessage
        {
            Pengirim = pengirim,
            Penerima = penerima,
            Subject = subject,
            Isi = message,
            StatusApproval = false
        });
        Console.WriteLine("Input berhasil disimpan.");
    }
    private void MenuApprovalFiles()
    {
        Console.WriteLine("Menu Approval with Message - Silahkan Isi Data Berikut:");
        string pengirim = getInput("Masukan Nama Pengirim: ");
        string penerima = getInput("Masukan Nama Penerima: ");
        string subject = getInput("Masukan Subject: ");
        string message = getInput("Masukan Message: ");
        // string status = getInput("Approve (Y/N): ");
        string files = getInput("Upload Files: ");
    }
    #endregion

    #region MenuLihatMessage

    private void showMessageList()
    {
        Console.WriteLine("Message List:");

        int i = 1;
        foreach (var listMessage in messages)
        {
            // Console.WriteLine($"{i}. Pengirim: {listMessage.Pengirim}, Penerima: {listMessage.Penerima}, Subject: {listMessage.Subject}, Isi: {listMessage.Isi}, Status Approval: {(listMessage.ApprovalStatus ? "Approved" : "Not Approved")}");
            Console.WriteLine($"{i}. Pengirim: {listMessage.Pengirim}, Subject: {listMessage.Subject}, Status Approval: {(listMessage.ApprovalStatus ? "Approved" : "Not Approved")}");
            i++;

            int selected = parseInput(getInput("Pilih Message untuk melihat detail"));
            try
            {
                if (selected < 1 || selected > messages.Count)
                {
                    Console.WriteLine("Pilihan tidak valid, silakan coba lagi.");
                }
                else
                {
                    var selectedMessage = messages[selected - 1];
                    Console.WriteLine($"Pengirim: {selectedMessage.Pengirim}");
                    Console.WriteLine($"Penerima: {selectedMessage.Penerima}");
                    Console.WriteLine($"Subject: {selectedMessage.Subject}");
                    Console.WriteLine($"Isi: {selectedMessage.Isi}");
                    if (selectedMessage is ApprovalMessage)
                    {
                        
                    }
                }
            }
            catch
            {
                
            }
        }
    }
#endregion
    
    public void Run()
    {
        showMainMenu();
    }
}