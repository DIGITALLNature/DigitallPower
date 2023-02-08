// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.maintenance.Model.Serialization;

public class Carriers : List<Carrier>
{
    public Carriers()
    {

    }
    public Carriers(IEnumerable<Carrier> carriers) : base(carriers)
    {
    }
}
