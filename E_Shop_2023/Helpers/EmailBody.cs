namespace E_Shop_2023.Helpers
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Forgot Password</title>
</head>
<body style=""font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;"">

<div style=""max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px;"">
    <h2 style=""color: #333;"">Forgot Password</h2>
    <p>Hello,</p>
    <p>You are receiving this email because you have requested to reset your password for your account.</p>
    <p>Please click the link below to reset your password:</p>
    <p><a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;"">Reset Password</a></p>
    <p>If you did not request a password reset, please ignore this email.</p>
    <p>Thank you.</p>
    <p style=""font-size: 12px; color: #777;"">If you encounter any issues, please contact us via email: support@example.com</p>
</div>

</body>
</html>
";
        }
    }
}
