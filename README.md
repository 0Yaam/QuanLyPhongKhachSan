## How to Run the Project

1. Open the solution in Visual Studio 2022.
2. Copy the database connection string from:  `...\QuanLyPhongKhachSan\QuanLyPhongKhachSan.DAL\database\HotelManager.mdf`
3. Paste it into the file:  `QuanLyPhongKhachSan.DAL\Config.cs`  
4. Run

## Default Login Accounts

**Admin**  
  Username: `dan`  
  Password: `123`

**Staff**  
  Username: `thm`  
  Password: `123`

## If Build Fails with File Locked Error (MSB3021/MSB27)
``The process cannot access the file because it is being used by another process.``

This happens when the program is still running in the background.

**Fix:**
1. In Visual Studio, press `Ctrl + ~` to open the terminal.
2. Run this command:

```bash
taskkill /IM QuanLyPhongKhachSan.exe /F
```

## Require

SQL Server LocalDB