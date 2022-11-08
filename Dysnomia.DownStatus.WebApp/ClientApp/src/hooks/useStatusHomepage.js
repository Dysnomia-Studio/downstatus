import { useEffect, useState } from 'react';

export default function useStatusHomepage() {
	const [results, setResults] = useState([]);

	useEffect(() => {
		(async() => {
			const res = await fetch('/api/status/homepage');
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
	}, []);

	return results;
}