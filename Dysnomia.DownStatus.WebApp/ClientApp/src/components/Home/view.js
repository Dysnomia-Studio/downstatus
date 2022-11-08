import { useState } from 'react';

import ListItem from '../ListItem';
import Searchbar from '../Searchbar';

export default function Home({ useStatusHomepage, useSearch }) {
	const [searchData, setSearchData] = useState('');
	const statusHomePage = useStatusHomepage();
	const statusSearch = useSearch(searchData);
	const statuses = (searchData.length < 3) ? statusHomePage : statusSearch;

	let unhealthyDOM = null;
	const unhealthyData = statuses.filter((x) => x.status === 'Unhealthy');
	if(unhealthyData.length > 0) {
		unhealthyDOM = (
			<div>
				{unhealthyData.map(x => <ListItem element={x} key={x.appId} className="unhealthy" additionalData={<p>✗</p>} />)}
			</div>
		);
	}

	const healthyData = statuses.filter((x) => x.status !== 'Unhealthy');
	return (
		<div className="main">
			<h1>Downstat.us</h1>
			<Searchbar onChange={setSearchData} />

			{unhealthyDOM}

			{healthyData.map(x => <ListItem element={x} key={x.appId} className="healthy" additionalData={<p>✓</p>} />)}
		</div>
	);
}