import { XrmMockFormODataFilter } from '../generated/TypeScript/xrm_mock_form_odata_filter';

describe('XrmMockFormODataFilter', () => {
  it('returns all fields when no $select is provided', () => {
    const data = [
      { dgt_test_tableid: '1', name: 'Alpha', statuscode: 1 },
      { dgt_test_tableid: '2', name: 'Beta', statuscode: 2 },
    ];

    const result = XrmMockFormODataFilter.executeRetrieveMultipleRecord(
      'dgt_test_table',
      data,
      '$filter=statuscode eq 1'
    );

    expect(result).toHaveLength(1);
    expect(result[0]).toMatchObject({
      dgt_test_tableid: '1',
      name: 'Alpha',
      statuscode: 1,
    });
  });

  it('selects only requested fields plus id', () => {
    const data = [
      { dgt_test_tableid: '1', name: 'Alpha', statuscode: 1 },
    ];

    const result = XrmMockFormODataFilter.executeRetrieveMultipleRecord(
      'dgt_test_table',
      data,
      '$select=name&$filter=statuscode eq 1'
    );

    expect(result[0]).toHaveProperty('dgt_test_tableid');
    expect(result[0]).toHaveProperty('name');
    expect(result[0]).not.toHaveProperty('statuscode');
  });

  it('handles null comparisons with strict equality semantics', () => {
    const data = [
      { dgt_test_tableid: '1', name: null },
      { dgt_test_tableid: '2', name: 'Alpha' },
    ];

    const result = XrmMockFormODataFilter.executeRetrieveMultipleRecord(
      'dgt_test_table',
      data,
      '$filter=name eq null'
    );

    expect(result).toHaveLength(1);
    expect(result[0].dgt_test_tableid).toBe('1');
  });

  it('returns the full list when no options are provided', () => {
    const data = [
      { dgt_test_tableid: '1', name: 'Alpha' },
      { dgt_test_tableid: '2', name: 'Beta' },
    ];

    const result = XrmMockFormODataFilter.executeRetrieveMultipleRecord(
      'dgt_test_table',
      data,
      ''
    );

    expect(result).toHaveLength(2);
  });
});
