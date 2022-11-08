import { useEffect, useState } from 'react';

export default function useStatusHomepage(searchTerm) {
	const [results, setResults] = useState([]);

	useEffect(() => {
		if(searchTerm.length < 3) {
			return;
		}

		(async() => {
			const res = await fetch(`/api/status/search/${searchTerm}`);
			const data = await res.json();
			data.sort((a, b) => {
				if(a.appName > b.appName) {
					return 1;
				}
				if(a.appName < b.appName) {
					return -1;
				}
				return 0;
			});

			setResults(data);
		})();
	}, [searchTerm]);

	return results;
}