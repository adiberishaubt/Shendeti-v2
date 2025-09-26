import React, { useState, useMemo } from 'react';
import { MagnifyingGlassIcon, FunnelIcon } from '@heroicons/react/24/outline';

interface SearchFilterProps {
  onSearch: (query: string) => void;
  onFilter: (filters: Record<string, any>) => void;
  placeholder?: string;
  filters?: FilterOption[];
  className?: string;
}

interface FilterOption {
  key: string;
  label: string;
  type: 'select' | 'date' | 'number' | 'text';
  options?: { value: string | number; label: string }[];
  placeholder?: string;
}

const SearchFilter: React.FC<SearchFilterProps> = ({
  onSearch,
  onFilter,
  placeholder = 'Search...',
  filters = [],
  className = '',
}) => {
  const [searchQuery, setSearchQuery] = useState('');
  const [filterValues, setFilterValues] = useState<Record<string, any>>({});
  const [showFilters, setShowFilters] = useState(false);

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const query = e.target.value;
    setSearchQuery(query);
    onSearch(query);
  };

  const handleFilterChange = (key: string, value: any) => {
    const newFilters = { ...filterValues, [key]: value };
    setFilterValues(newFilters);
    onFilter(newFilters);
  };

  const clearFilters = () => {
    setFilterValues({});
    onFilter({});
  };

  const hasActiveFilters = Object.values(filterValues).some(value => 
    value !== '' && value !== null && value !== undefined
  );

  return (
    <div className={`space-y-4 ${className}`}>
      {/* Search Bar */}
      <div className="relative">
        <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
          <MagnifyingGlassIcon className="h-5 w-5 text-gray-400" />
        </div>
        <input
          type="text"
          value={searchQuery}
          onChange={handleSearchChange}
          className="block w-full pl-10 pr-3 py-2 border border-gray-300 rounded-md leading-5 bg-white placeholder-gray-500 focus:outline-none focus:placeholder-gray-400 focus:ring-1 focus:ring-primary-500 focus:border-primary-500"
          placeholder={placeholder}
        />
      </div>

      {/* Filter Toggle */}
      {filters.length > 0 && (
        <div className="flex items-center justify-between">
          <button
            onClick={() => setShowFilters(!showFilters)}
            className="inline-flex items-center px-3 py-2 border border-gray-300 shadow-sm text-sm leading-4 font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500"
          >
            <FunnelIcon className="h-4 w-4 mr-2" />
            Filters
            {hasActiveFilters && (
              <span className="ml-2 inline-flex items-center px-2 py-0.5 rounded-full text-xs font-medium bg-primary-100 text-primary-800">
                Active
              </span>
            )}
          </button>

          {hasActiveFilters && (
            <button
              onClick={clearFilters}
              className="text-sm text-gray-500 hover:text-gray-700"
            >
              Clear filters
            </button>
          )}
        </div>
      )}

      {/* Filter Options */}
      {showFilters && filters.length > 0 && (
        <div className="bg-gray-50 p-4 rounded-lg space-y-4">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
            {filters.map((filter) => (
              <div key={filter.key}>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  {filter.label}
                </label>
                {filter.type === 'select' && (
                  <select
                    value={filterValues[filter.key] || ''}
                    onChange={(e) => handleFilterChange(filter.key, e.target.value)}
                    className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500"
                  >
                    <option value="">All</option>
                    {filter.options?.map((option) => (
                      <option key={option.value} value={option.value}>
                        {option.label}
                      </option>
                    ))}
                  </select>
                )}
                {filter.type === 'date' && (
                  <input
                    type="date"
                    value={filterValues[filter.key] || ''}
                    onChange={(e) => handleFilterChange(filter.key, e.target.value)}
                    className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500"
                  />
                )}
                {filter.type === 'number' && (
                  <input
                    type="number"
                    value={filterValues[filter.key] || ''}
                    onChange={(e) => handleFilterChange(filter.key, e.target.value)}
                    placeholder={filter.placeholder}
                    className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500"
                  />
                )}
                {filter.type === 'text' && (
                  <input
                    type="text"
                    value={filterValues[filter.key] || ''}
                    onChange={(e) => handleFilterChange(filter.key, e.target.value)}
                    placeholder={filter.placeholder}
                    className="block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-primary-500 focus:border-primary-500"
                  />
                )}
              </div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

// Generic search hook
export const useSearch = <T>(
  data: T[],
  searchFields: (keyof T)[],
  initialQuery: string = ''
) => {
  const [query, setQuery] = useState(initialQuery);

  const filteredData = useMemo(() => {
    if (!query.trim()) return data;

    const lowercaseQuery = query.toLowerCase();
    return data.filter((item) =>
      searchFields.some((field) => {
        const value = item[field];
        return value && String(value).toLowerCase().includes(lowercaseQuery);
      })
    );
  }, [data, query, searchFields]);

  return {
    query,
    setQuery,
    filteredData,
  };
};

// Generic filter hook
export const useFilters = <T>(
  data: T[],
  filterConfig: Record<string, (item: T, value: any) => boolean>
) => {
  const [filters, setFilters] = useState<Record<string, any>>({});

  const filteredData = useMemo(() => {
    return data.filter((item) =>
      Object.entries(filters).every(([key, value]) => {
        if (!value || value === '') return true;
        const filterFn = filterConfig[key];
        return filterFn ? filterFn(item, value) : true;
      })
    );
  }, [data, filters, filterConfig]);

  const updateFilter = (key: string, value: any) => {
    setFilters(prev => ({ ...prev, [key]: value }));
  };

  const clearFilters = () => {
    setFilters({});
  };

  const hasActiveFilters = Object.values(filters).some(value => 
    value !== '' && value !== null && value !== undefined
  );

  return {
    filters,
    updateFilter,
    clearFilters,
    filteredData,
    hasActiveFilters,
  };
};

// Combined search and filter hook
export const useSearchAndFilter = <T>(
  data: T[],
  searchFields: (keyof T)[],
  filterConfig: Record<string, (item: T, value: any) => boolean>,
  initialQuery: string = ''
) => {
  const { query, setQuery, filteredData: searchResults } = useSearch(data, searchFields, initialQuery);
  const { filters, updateFilter, clearFilters, filteredData, hasActiveFilters } = useFilters(searchResults, filterConfig);

  return {
    query,
    setQuery,
    filters,
    updateFilter,
    clearFilters,
    filteredData,
    hasActiveFilters,
  };
};

export default SearchFilter;
