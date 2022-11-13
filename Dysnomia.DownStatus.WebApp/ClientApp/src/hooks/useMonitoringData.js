import { useEffect, useState } from 'react';

export default function useMonitoringData(key) {
	const [results, setResults] = useState(null);

	useEffect(() => {
		if(key.length < 3) {
			return;
		}

		(async() => {
			const res = await fetch(`/api/status/byKey/${key}`);
			if(res.status !== 200) {
				return;
			}

			setResults(await res.json());
		})();
	}, [key]);

	return results;
}