import { Link, useParams } from 'react-router-dom';

import useMonitoringData from '../../hooks/useMonitoringData';

import './index.css';

export default function Details() {
	const { key } = useParams();
	const app = useMonitoringData(key);

	if(!app) {
		return null;
	}

	let history = app.statusList
		.map((x) => {
			x.date = new Date(x.date);
			return x;
		});
	history.sort((a, b) => b.date.getTime() - a.date.getTime());

	let getStatusDisplay = (entry) => {
		return entry.status != 'Alive' ? <p className="details-unhealthy">✗</p> : <p className="details-healthy">✓</p>; 
	};

	let getStatus = (targetName) => {
		let localHistory = history.filter((x) => x.targetName == targetName);
		return getStatusDisplay(history[0]); 
	};

	return (
		<>
			<div>
				<Link to="/" className="details-home-link">←</Link>
			</div>
			<div className="details-half">
				<h2>{app.appName}</h2>
				<p>{app.description}</p>
				{app.logo && <div className="details-image">
					<img alt={app.appName} src={app.logo} />
				</div>}

				<table className="services-table">
					<tbody>
					{app.targets.map((x) =>
						(<tr key={x.name}>
							<td><a href={x.target}>{x.name}</a></td>
							<td>{getStatus(x.name)}</td>
						</tr>)
					)}
					</tbody>
				</table>
			</div>

			<div className="details-half">
				<h2>History</h2>
				<table>
					<tbody>
					{history.map((x) =>
						(<tr key={x.name}>
							<td>{(new Date(x.date)).toLocaleDateString()} {(new Date(x.date)).toLocaleTimeString()}</td>
							<td>{x.targetName}</td>
							<td>{getStatusDisplay(x)}</td>
						</tr>)
					)}
					</tbody>
				</table>
			</div>
		</>
	);
}