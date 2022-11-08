import './index.css';

export default function SearchBar() {
	return (
		<div className="search-bar-parent">
			<label for="search-bar" className="search-bar-label">🔎</label>
			<input type="text" className="search-bar" autocomplete={false} placeholder="Is ... down ?" />
		</div>
	);
}