using BCrypt.Net;

namespace System;

public static class Hashing
{
	public static string HashPassword(string password)
	{
		return BCrypt.Net.BCrypt.HashPassword(password);
	}

	public static bool VerifyPassword(string password, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
	}
}
