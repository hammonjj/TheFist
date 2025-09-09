# The Fist

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/YOUR_USERNAME/parcel-corps.git
cd the-fist
```

#### 1b. Install Git LFS via Homebrew if you never have
```bash
brew install git-lfs
git lfs install
```

### 2. Install Git LFS (once per machine)

```bash
git lfs install
```

### 3. Pull LFS-tracked files

```bash
git lfs pull
```

### 4. Open in Unity

Use the Unity Hub or open the `/TheFist` folder directly.

---

## Git LFS Info

We use Git LFS to manage large binary files including:

- Unity scene/prefab assets
- Spine `.skel`, `.atlas`, `.json`
- Krita `.kra`
- Inkscape `.svg` (if large)

To add new file types to LFS:

```bash
git lfs track "*.EXT"
git add .gitattributes
git commit -m "Track new LFS extension"
```

---

## Contributing

- Make sure `git lfs install` has been run before working with this repo.
- Avoid merge conflicts by coordinating asset changes.
- Do not manually merge `.unity`, `.prefab`, or binary assets.

## License

This project is **not licensed for reuse**. Code, assets, and artwork are proprietary and may not be copied or reused.
