import { SearchProps } from "./types";
import './styles.css';
import { useRef, useState } from "react";

function Search({ onSearchAction }: SearchProps) {
    const inputRef = useRef<HTMLInputElement>(null);
    const [searchValue, setSearchValue] = useState<string>('');

    const handleSearch = () => {
        const inputedValue = inputRef?.current?.value || '';
            if (inputedValue !== searchValue) {
                setSearchValue(inputedValue);
                onSearchAction(inputedValue);
            }
        };



    return (<>
        <div className="search-container">
            <input
                className="search-input"
                ref={inputRef}
                type="text"
                placeholder="Search here..." />
            <button
                className="search-button"
                onClick={handleSearch}
            >
                Search
            </button>
        </div>
    </>
    );
}

export default Search;
