// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.documenttemplates.Configuration;

namespace dgt.power.documenttemplates.Logic;

public sealed record PreparedDocumentTemplate(DocumentTemplateEntry Entry, Guid TargetId, bool Create, ReadOnlyMemory<byte> Content);
