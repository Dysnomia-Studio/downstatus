import './index.css';

export default function ListItem({ additionalData, className, element }) {
	const style = { 
		backgroundImage: element.logo && `url(${element.logo})`,
		border: !element.logo && '1px solid white',
	}

	return (
		<div className={className + ' list-item'} style={style}>
			{!element.logo && element.appName}
			{additionalData}
		</div>
	);
}