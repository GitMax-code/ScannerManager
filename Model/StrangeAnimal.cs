﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerManager.Model;
public class StrangeAnimal
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Picture { get; set; } = string.Empty;
    public int ModificationCount { get; set; } = 0;
}
