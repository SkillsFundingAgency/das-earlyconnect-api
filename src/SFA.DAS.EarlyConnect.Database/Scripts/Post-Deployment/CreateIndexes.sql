IF NOT EXISTS (SELECT *  FROM sys.indexes WHERE name='IX_LEPSCoverage_Postcode'
    AND object_id = OBJECT_ID('dbo.LEPSCoverage'))
  BEGIN
    CREATE INDEX IX_LEPSCoverage_Postcode ON [LEPSCoverage] (Postcode)
  END