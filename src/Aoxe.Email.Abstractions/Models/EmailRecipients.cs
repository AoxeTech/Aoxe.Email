﻿namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailRecipients
{
    public List<EmailAddress> To { get; set; } = [];
    public List<EmailAddress> Cc { get; set; } = [];
    public List<EmailAddress> Bcc { get; set; } = [];
}
