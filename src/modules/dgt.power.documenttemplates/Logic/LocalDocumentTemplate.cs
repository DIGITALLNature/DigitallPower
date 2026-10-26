// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.documenttemplates.Configuration;

namespace dgt.power.documenttemplates.Logic;

public sealed record LocalDocumentTemplate(DocumentTemplateEntry Entry, ReadOnlyMemory<byte> Content);
