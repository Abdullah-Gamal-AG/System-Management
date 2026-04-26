namespace System;

public class DataUser
{
	public static string Id { get; set; } = "";

	public static string Name { get; set; } = "";

	public static string Role { get; set; } = "";

	public static string Email { get; set; } = "";

	public static string Password { get; set; } = "";

	public static bool IsLoggedIn { get; set; } = false;

	public static void Clear()
	{
		Id = "";
		Name = "";
		Role = "";
		Email = "";
		Password = "";
		IsLoggedIn = false;
	}
}
