import { Link } from 'react-router-dom';

import './index.css';

export default function ListItem({ additionalData, className, element }) {
	const style = { 
		backgroundImage: element.logo && `url(${element.logo})`,
		border: !element.logo && '1px solid white',
	}

	return (
		<Link to={`/details/${element.appId}`}>
			<div className={className + ' list-item'} style={style} aria-label={element.appName}>
				{!element.logo && element.appName}
				{additionalData}
			</div>
		</Link>
	);
}