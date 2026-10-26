// Copyright (c) DIGITALL Nature. All rights reserved

namespace dgt.power.documenttemplates.Configuration;

public sealed record DocumentTemplateEntry(
    Guid SourceId,
    string Name,
    string DocumentType,
    string Entity,
    string File,
    int LanguageCode,
    string? Description)
{
    public int TypeCode => DocumentType == "word" ? 2 : 1;

    public string Identity => $"{Name}\0{DocumentType}\0{Entity}";
}
