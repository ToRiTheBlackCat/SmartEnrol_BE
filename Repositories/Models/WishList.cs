﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Repositories.Models;

public partial class WishList
{
    public int WishListId { get; set; }

    public int AccountId { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual Account Account { get; set; }

    public virtual ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();
}