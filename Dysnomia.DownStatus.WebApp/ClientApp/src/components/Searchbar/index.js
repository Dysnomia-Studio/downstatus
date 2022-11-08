import './index.css';

export default function SearchBar({ onChange }) {
	return (
		<div className="search-bar-parent">
			<label htmlFor="search-bar" className="search-bar-label">🔎</label>
			<input type="text" className="search-bar" onChange={(e) => onChange(e.target.value, e)} placeholder="Is ... down ?" />
		</div>
	);
}