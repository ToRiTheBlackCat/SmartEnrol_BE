﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SmartEnrol.Repositories.Models;

public partial class Characteristic
{
    public int CharacteristicId { get; set; }

    public string CharacteristicName { get; set; }

    public virtual ICollection<CharacteristicOfMajor> CharacteristicOfMajors { get; set; } = new List<CharacteristicOfMajor>();

    public virtual ICollection<CharacteristicOfStudent> CharacteristicOfStudents { get; set; } = new List<CharacteristicOfStudent>();
}