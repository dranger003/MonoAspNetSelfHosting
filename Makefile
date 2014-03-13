all: WebHost.dll Program.exe
	cp WebHost.dll bin/

WebHost.dll: WebHost.cs
	mcs /t:library /pkg:dotnet WebHost.cs

Program.exe: Program.cs
	mcs /out:$@ /pkg:dotnet /r:WebHost.dll Program.cs

clean:
	rm -f *.dll *.exe
