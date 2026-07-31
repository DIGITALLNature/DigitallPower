/** @type {import('ts-jest').JestConfigWithTsJest} */
module.exports = {
  preset: 'ts-jest',
  testEnvironment: 'node',
  roots: ['<rootDir>/tests'],
  testMatch: ['**/*.test.ts'],
  transform: {
    '^.+\\.ts$': [
      'ts-jest',
      {
        tsconfig: '<rootDir>/tsconfig.json',
        diagnostics: {
          ignoreCodes: [2307, 2322, 2339, 2345, 2749, 7006],
        },
      },
    ],
  },
  moduleNameMapper: {
    '^@generated/(.*)$': '<rootDir>/generated/$1',
  },
};
