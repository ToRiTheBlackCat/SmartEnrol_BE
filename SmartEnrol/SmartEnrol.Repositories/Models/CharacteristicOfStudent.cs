﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartEnrol.Repositories.Models;

public partial class CharacteristicOfStudent
{
    public int CharacteristicOfStudentId { get; set; }

    public int CharacteristicId { get; set; }

    public int AccountId { get; set; }

    public int Score { get; set; }

    public virtual Account Account { get; set; }

    public virtual Characteristic Characteristic { get; set; }
}